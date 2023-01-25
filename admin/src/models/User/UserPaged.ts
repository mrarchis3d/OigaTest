export interface UserPaged {
  pageNumber: number;
  pageSize: number;
  searchWords?: string;
  sortBy?: string;
  sortDirection?: string;
}
