import { Component, OnInit } from '@angular/core';
import { ResumeService } from 'src/app/services/resume.service';
import { IResume } from 'src/app/models/resume/IResume';
import { from, map, mergeMap, groupBy, reduce } from 'rxjs';
import { ResumeChildrenInteractionService } from 'src/app/services/resume-children-interaction.service';
import { ISection } from 'src/app/models/resume/ISection';
import { SectionType } from 'src/app/enums/SectionType';
import { ISectionTypeSectionPair } from 'src/app/models/resume/ISectionTypeSectionPair';

@Component({
  selector: 'app-resume-admin',
  templateUrl: './resume-admin.component.html',
  styleUrls: ['./resume-admin.component.scss']
})
export class ResumeAdminComponent implements OnInit {
  public resume: IResume = {} as IResume;
  public sections: Array<ISectionTypeSectionPair> = [];

  constructor(private resumeService: ResumeService, private resumeSectionInteractionService: ResumeChildrenInteractionService) { }

  ngOnInit(): void {
    this.resumeService.GetMainResume()
      .subscribe(
        x => {
          this.ResumeUpdate(x);
        });
  };

  public SaveResume(): void {
    this.resumeSectionInteractionService.updateChildren();
    this.resume.sections = this.sections.map(x => x.sections).flat();
    this.resumeService.SaveResume(this.resume).subscribe(
      x => {
        this.ResumeUpdate(x);
      });
  }

  public AddNewSectionType(): void {
    var section = <ISectionTypeSectionPair>{};
    section.sectionType = SectionType.None;
    section.sections = Array<ISection>();
    this.sections.push(section);
  }

  public DeleteSectionTypeFromForm(index: number): void {
    this.sections.splice(index, 1);
  }

  private ResumeUpdate(x: IResume) {
    if (x != null) {
      this.resume = x;
      this.sections = [];

      from(x.sections)
        .pipe(
          groupBy(y => y.sectionType),
          mergeMap(group$ => group$.pipe(reduce((acc, cur) => [...acc, cur], <any>[group$.key]))),
          map(arr => ({ sectionType: arr[0], sections: arr.slice(1) }))
        )
        .subscribe(x => {
          var section = <ISectionTypeSectionPair>{};
          section.sectionType = x.sectionType;
          section.sections = x.sections;
          this.sections.push(section);
        });
    }
  }
}
