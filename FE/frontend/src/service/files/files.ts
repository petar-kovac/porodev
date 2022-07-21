import api from '../base';
import { IFilesRequest, IProfileRequest } from './files.props';

export const findFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Storage/Read')
    .then((res) => res.data);
};

export const verifyEmail: (email: any, token: any) => Promise<any> = (
  email,
  token,
) => {
  return api.service().post(`/api/User/Verify?Email=${email}&Token=${token}`);
};

export const downloadFile: (fileId: string) => Promise<any> = (fileId) => {
  return api
    .service()
    .get(`/api/Storage/Download?fileId=${fileId}`, {
      responseType: 'blob',
    })
    .then((res) => res);
};

export const deleteFile: (fileId: string) => Promise<any> = (fileId) => {
  return api
    .service()
    .delete(`/api/Storage/Delete?fileId=${fileId}`)
    .then((res) => res);
};

export const postFiles: (payload: IFilesRequest) => Promise<string> = (
  payload,
) => {
  return api
    .service()
    .post('/api/files', payload)
    .then((res) => res.data);
};

export const postProfile: (payload: IProfileRequest) => Promise<any> = (
  payload,
) => {
  return api
    .service()
    .put('/api/User/UpdateUser', { ...payload, passwordUnhashed: 'Srdjan123!' })
    .then((res) => res.data);
};
