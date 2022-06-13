import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ISection } from 'src/app/models/resume/ISection';
import { SectionType } from 'src/app/enums/SectionType';
import { SectionTypeNames } from '../SectionTypeNames';
import { FormControl, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { ResumeChildrenInteractionService } from 'src/app/services/resume-children-interaction.service';
import { ISectionTypeSectionPair } from 'src/app/models/resume/ISectionTypeSectionPair';

@Component({
  selector: 'app-section-admin',
  templateUrl: './section-admin.component.html',
  styleUrls: ['./section-admin.component.scss']
})
export class SectionAdminComponent implements OnInit {
  @Input() sections: ISectionTypeSectionPair = <ISectionTypeSectionPair>{};
  public sectionTypeNames: Map<SectionType, string>;
  public form: FormGroup;
  public control: FormArray;

  constructor(private fb: FormBuilder, private resumeChildrenInteractionService: ResumeChildrenInteractionService) {
    this.sectionTypeNames = SectionTypeNames.GetSectionTypeNames();
    this.form = this.fb.group({
      sectionsArray: this.fb.array([])
    });
    this.control = <FormArray>this.form.get('sectionsArray') as FormArray;

    resumeChildrenInteractionService.saveActivated$.subscribe(
      () => {
        this.updateSectionsWithFormValues();
      });
  }

  ngOnInit(): void {
    this.fillForm();
  }

  public DeleteSectionFromForm(index: number): void {
    this.control.removeAt(index);
  }

  public AddSectionToForm(): void {
    this.control.push(this.fb.group({
      company: "",
      content: "",
      description: "",
      from: [new Date()],
      id: 0,
      jobTitle: "",
      location: "",
      separator: "",
      title: "",
      to: [new Date()]
    }))
  }

  public GetSectionTypeName(sectionType: SectionType): string | undefined {
    return SectionTypeNames.GetSectionTypeName(sectionType);
  }

  private fillForm(): void {
    this.sections.sections.forEach((section) => {
      this.control.push(this.fb.group({
        company: [section.company],
        content: [section.content],
        description: [section.description],
        from: [new Date(section.from)],
        id: [section.id],
        jobTitle: [section.jobTitle],
        location: [section.location],
        separator: [section.separator],
        title: [section.title],
        to: [new Date(section.to)],
      }));
    })
  }

  private updateSectionsWithFormValues(): void {
    this.sections.sections = []
    this.control.controls.forEach(formSection => {
      var newSection = {} as ISection;
      newSection.id = formSection.get('id')?.value
      newSection.title = formSection.get('title')?.value;
      newSection.company = formSection.get('company')?.value;
      newSection.description = formSection.get('description')?.value;
      newSection.jobTitle = formSection.get('jobTitle')?.value;
      newSection.location = formSection.get('location')?.value;
      newSection.from = formSection.get('from')?.value;
      newSection.to = formSection.get('to')?.value;
      newSection.content = formSection.get('content')?.value;
      newSection.separator = formSection.get('separator')?.value;
      newSection.sectionType = this.sections.sectionType;

      this.sections.sections.push(newSection)
    });
  }
}
