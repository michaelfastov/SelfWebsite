import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PixivLinksSignalrService } from '../../services/pixiv-links-signalr.service';
import { ILinkPreview } from 'src/app/models/ILinkPreview';
import { LinkPreviewService } from 'src/app/services/link-preview.service';

@Component({
  selector: 'app-pixiv-links',
  templateUrl: './pixiv-links.component.html',
  styleUrls: ['./pixiv-links.component.scss']
})
export class PixivLinksComponent implements OnInit, OnDestroy {
  links: ILinkPreview[] = [];
  allFeedSubscription: any;

  constructor(private pixivLinksSignalrService: PixivLinksSignalrService, private linkPreviewService: LinkPreviewService) { }

  ngOnInit(): void {
    this.pixivLinksSignalrService.startConnection().then(() => {
      this.pixivLinksSignalrService.listen();
      this.allFeedSubscription = this.pixivLinksSignalrService.AllFeedObservable
        .subscribe((res: string) => {
          this.OnPreview(res)
        });
    });
  }

  OnPreview(link: string) {
    this.linkPreviewService.GetLinkPreview(link)
      .subscribe({
        next: (response) => {
          let preview: ILinkPreview = response;
          this.links.push(preview);
        }
      });
  }

  ngOnDestroy(): void {
    (<Subscription>this.allFeedSubscription).unsubscribe();
  }
}
