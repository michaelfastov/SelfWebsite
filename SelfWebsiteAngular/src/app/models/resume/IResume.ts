import { ISection } from "./ISection";
import { ILink } from "./ILink";

export interface IResume {
    id: number;
    name: string;
    title: String;
    description: string;
    sections: Array<ISection>;
    links: Array<ILink>
}