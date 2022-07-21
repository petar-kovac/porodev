import api from '../base';
import {
  IFindUserByIdRequest,
  IFindUserByIdResponse,
  ILoginRequest,
  ILoginResponse,
  IRegisterRequest,
  IRegisterResponse,
} from './authorization.props';

export const loginApi: (payload: ILoginRequest) => Promise<ILoginResponse> = (
  payload,
) => {
  return api
    .service()
    .post('/api/User/LoginUser', payload)
    .then((response) => response.data);
};
export const registerApi: (
  payload: IRegisterRequest,
) => Promise<IRegisterResponse> = (payload) => {
  return api
    .service()
    .post('/api/User/register/user', payload)
    .then((response) => response.data);
};

export const findUserById: (id: string) => Promise<any> = (id) => {
  return api
    .service()
    .get(`/api/User/ReadUserById?id=${id}`)
    .then((res) => res);
};
