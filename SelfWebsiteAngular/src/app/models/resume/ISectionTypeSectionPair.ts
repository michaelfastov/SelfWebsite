import { SectionType } from "src/app/enums/SectionType";
import { ISection } from "./ISection";

export interface ISectionTypeSectionPair {
    sectionType: SectionType
    sections: ISection[];
}