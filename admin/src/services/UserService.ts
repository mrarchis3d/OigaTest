import { User } from '../models/User/User';
import { UserEntity } from '../models/User/UserEntity';
import { UserPaged } from '../models/User/UserPaged';
import { PageResult } from '../models/Utils/PageResult';
import { get, post } from './api'


export const GetFilteredUsers = async (body: UserPaged) => {
    return await post<PageResult<User>>('/User/GetFiltered', body);
  };

  export const GetUserById = async (id: number) => {
    return await get<UserEntity>('/User/'+id);
  };