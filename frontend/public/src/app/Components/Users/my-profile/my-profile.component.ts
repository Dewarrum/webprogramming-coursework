import { Component, OnInit } from '@angular/core';
import {ApiService} from '../../../Api/api.service';
import {UserProfileData} from '../../../Models/UserProfileData';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
  public userProfileData: UserProfileData;
  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.getMyUserProfileData().subscribe(s => {
      this.userProfileData = s;
    }, error => {
      console.log(error);
    });
  }

}
