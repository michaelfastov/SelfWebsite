import { LinkType } from "src/app/enums/LinkType";

export interface ILink {
    id: number;
    linkType: LinkType;
    linkAddress: String;
}