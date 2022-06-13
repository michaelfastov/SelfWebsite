import { Component, OnInit, Input } from '@angular/core';
import { ISection } from 'src/app/models/resume/ISection';
import { SectionType } from 'src/app/enums/SectionType';
import { SectionTypeNames } from '../SectionTypeNames';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.scss']
})
export class SectionComponent implements OnInit {
  @Input() sectionType: SectionType = 0;
  @Input() sections: ISection[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  public GetSectionTypeName(sectionType: SectionType): string | undefined {
    return SectionTypeNames.GetSectionTypeName(sectionType);
  }
}
