import {Component, OnInit} from '@angular/core';
import {UserPreviewData} from '../../../Models/UserPreviewData';
import {ApiService} from '../../../Api/api.service';

@Component({
  selector: 'app-followers-table-component',
  templateUrl: './followers-table.component.html',
  styleUrls: ['./followers-table.component.css']
})
export class FollowersTableComponent implements OnInit {
  followeInfos: UserPreviewData[];
  currentUserLogin: string;
  public loading: boolean;
  constructor(private api: ApiService) {
    this.loading = false;
    this.currentUserLogin = api.currentUserValue.login;
  }

  ngOnInit(): void {
    this.loading = true;
    this.api.getFollowers()
      .subscribe(res => {
        this.followeInfos = res.followers;
        this.loading = false;
      });
  }

}
