import { Component, OnInit, Input } from '@angular/core';
import { LinkType } from 'src/app/enums/LinkType';
import { ILink } from 'src/app/models/resume/ILink';
import { LinkTypeNames } from '../LinkTypeNames';

@Component({
  selector: 'app-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class LinkComponent implements OnInit {
  @Input() links: ILink[] = [];

  get LinkType() {
    return LinkType;
  }
  
  constructor() { }

  ngOnInit(): void {
  }

  public GetLinkTypeName(linkType: LinkType): string | undefined {
    return LinkTypeNames.GetLinkTypeName(linkType);
  }
}
