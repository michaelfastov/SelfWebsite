import { SectionType } from "src/app/enums/SectionType";

export interface ISection {
    id: number;
    sectionType: SectionType
    title: String;
    description: String;
    content: String;
    jobTitle: String;
    company: String;
    location: String;
    separator: String;
    from: Date
    to: Date
}