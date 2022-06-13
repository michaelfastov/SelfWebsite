import { Component, OnInit } from '@angular/core';
import { ResumeService } from 'src/app/services/resume.service';
import { IResume } from 'src/app/models/resume/IResume';
import { from, map, mergeMap, groupBy, reduce } from 'rxjs';

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.scss']
})
export class ResumeComponent implements OnInit {
  public resume: IResume = <IResume>{};
  public sections: Array<any> = [];
  constructor(private resumeService: ResumeService) { }

  ngOnInit(): void {
    this.resumeService.GetMainResume().subscribe(
      x => {
        this.resume = x;

        from(x.sections)
          .pipe(
            groupBy(y => y.sectionType),
            mergeMap(group$ => group$.pipe(reduce((acc, cur) => [...acc, cur], <any>[group$.key]))),
            map(arr => ({ sectionType: arr[0], sections: arr.slice(1) }))
          )
          .subscribe(x => this.sections.push(x));
      });
  }
}
