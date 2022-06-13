import { Component, OnInit, Input } from '@angular/core';
import { LinkType } from 'src/app/enums/LinkType';
import { ILink } from 'src/app/models/resume/ILink';
import { LinkTypeNames } from '../LinkTypeNames';

@Component({
  selector: 'app-link-admin',
  templateUrl: './link-admin.component.html',
  styleUrls: ['./link-admin.component.scss']
})
export class LinkAdminComponent implements OnInit {
  @Input() links: Array<ILink> = [];
  public linkTypeNames: Map<LinkType, string>;

  constructor() {
    this.linkTypeNames = LinkTypeNames.GetLinkTypeNames();
   }

  ngOnInit(): void {
  }

  public DeleteLink(index: number): void {
    this.links.splice(index, 1);
  }

  public AddLink(): void {
    this.links.push({} as ILink);
  }
}
