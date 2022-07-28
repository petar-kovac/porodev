import api from '../base';

export const totalUploadedFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/Dashboard/TotalNumberOfUploadedFiles')
    .then((res) => res.data);
};

export const totalUsers: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalNumberOfUsers')
    .then((res) => res.data);
};
