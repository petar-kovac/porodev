import api from '../base';
import { IFilesRequest } from './files.props';

export const findFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Storage/Read')
    .then((res) => res.data);
};

export const downloadFile: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Storage/Download?fileId=62c6814f0c1713a7d2d8d00a', {
      responseType: 'blob',
    })
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
