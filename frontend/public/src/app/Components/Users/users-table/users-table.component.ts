import {Component, Input, OnInit} from '@angular/core';
import {UserPreviewData} from '../../../Models/UserPreviewData';
import {Routes} from '../../../Routes/routes';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit {
  @Input() userInfos: UserPreviewData[];
  public routes = Routes;
  constructor() { }

  ngOnInit(): void {
  }

}
