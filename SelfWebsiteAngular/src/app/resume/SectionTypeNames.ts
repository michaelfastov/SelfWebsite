import { SectionType } from "../enums/SectionType";

export class SectionTypeNames {
    private static sectionTypeNames= new Map<SectionType, string>([
        [SectionType.WorkExperience, "Work Experience"],
        [SectionType.Education, "Education"]
    ]);

    public static GetSectionTypeName(sectionType: SectionType): string | undefined {
        return SectionTypeNames.sectionTypeNames.get(sectionType);
    }

    public static GetSectionTypeNames(): Map<SectionType, string> {
        return this.sectionTypeNames;
    }
}