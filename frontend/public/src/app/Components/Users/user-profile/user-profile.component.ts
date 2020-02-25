import {Component, Input, OnInit} from '@angular/core';
import {UserProfileData} from '../../../Models/UserProfileData';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  @Input() userProfileData: UserProfileData;
  constructor() { }

  ngOnInit(): void {
  }

}
