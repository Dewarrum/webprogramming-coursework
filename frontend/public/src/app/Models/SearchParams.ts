import {UserPreviewData} from './UserPreviewData';

export interface SearchParams {
  skip: number;
  take: number;
  displayName: string;
  login: string;
  email: string;
}

export interface SearchModel {
  userInfos: UserPreviewData[];
  searchParams: SearchParams;
  totalPageCount: number;
  totalEntries: number;
  pageNumber: number;
}
