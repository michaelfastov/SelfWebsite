import { LinkType } from "../enums/LinkType";

export class LinkTypeNames {
    private static linkTypeNames= new Map<LinkType, string>([
        [LinkType.Email, "Email"],
        [LinkType.GitHub, "GitHub"],
        [LinkType.LinkedIn, "LinkedIn"]
    ]);

    public static GetLinkTypeName(linkType: LinkType): string | undefined {
        return LinkTypeNames.linkTypeNames.get(linkType);
    }

    public static GetLinkTypeNames(): Map<LinkType, string> {
        return this.linkTypeNames;
    }
}