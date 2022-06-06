import api from '../base';
import { IFilesRequest } from './files.props';

export const findFiles: () => Promise<string> = () => {
  return api.service().get('/api/files');
};
export const postFiles: (payload: IFilesRequest) => Promise<string> = (
  payload,
) => {
  return api
    .service()
    .post('/api/files', payload)
    .then((res) => res.data);
};
