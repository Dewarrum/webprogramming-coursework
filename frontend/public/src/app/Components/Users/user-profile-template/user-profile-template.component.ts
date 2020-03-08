import {Component, Input, OnInit} from '@angular/core';
import {UserProfileModel} from '../../../Models/Users/UserProfileModel';
import {Routes} from '../../../Routes/routes';

@Component({
    selector: 'app-user-profile-template',
    templateUrl: './user-profile-template.component.html',
    styleUrls: ['./user-profile-template.component.css']
})
export class UserProfileTemplateComponent implements OnInit {
    @Input() userProfileData: UserProfileModel;
    routes = Routes;
    constructor() { }

    ngOnInit(): void {
    }

}
