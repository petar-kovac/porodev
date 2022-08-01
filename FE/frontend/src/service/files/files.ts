import api from '../base';
import {
  IFilesRequest,
  IProfileRequest,
  IPasswordRequest,
} from './files.props';

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
    .put('/api/User/UpdateUser', { ...payload })
    .then((res) => res.data);
};

export const searchFiles: (fileName: string) => Promise<any> = (fileName) => {
  return api
    .service()
    .get(`/api/StorageQuery/files?FileName=${fileName}`)
    .then((res) => res.data);
};

export const postPassword: (payload: IPasswordRequest) => Promise<any> = (
  payload,
) => {
  return api
    .service()
    .put('/api/User/ChangePassword', { ...payload })
    .then((res) => res.data);
};

export const findNumberOfUploadedFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Dashboard/TotalNumberOfUploadedFiles')
    .then((res) => res.data);
};

export const findNumberOfDeletedFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Dashboard/TotalNumberOfDeletedFiles')
    .then((res) => res.data);
};

export const findNumberOfUsers: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Dashboard/TotalNumberOfUsers')
    .then((res) => res.data);
};

export const findTotalMemory: (numberOfMonths: number) => Promise<any> = (
  numberOfMonths,
) => {
  return api
    .service()
    .get('/api/Dashboard/TotalMemoryUsedForUploadPerMonth')
    .then((res) => res.data);
};

export const findTotalDownload: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Dashboard/TotalMemoryUsedForDownloadPerMonth')
    .then((res) => res.data);
};
