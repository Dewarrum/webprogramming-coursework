import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {UserPreviewData} from '../../../Models/UserPreviewData';
import {ApiService} from '../../../Api/api.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchForm: FormGroup;
  searching: boolean;
  searchResults: UserPreviewData;
  constructor(private api: ApiService,
              private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      skip: ['', Validators.required]
    });
  }

}
