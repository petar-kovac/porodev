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
export const totalRuntime: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalRunTimeForAllUsers')
    .then((res) => res.data);
};
export const deletedFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalNumberOfDeletedFiles')
    .then((res) => res.data);
};
export const runtimePerMonth: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalRunTimePerMonth')
    .then((res) => res.data);
};
export const memoryUsedForUpload: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalMemoryUsedForUploadPerMonth')
    .then((res) => res.data);
};
export const memoryUsedForDownload: () => Promise<any> = () => {
  return api
    .service()
    .get('api/Dashboard/TotalMemoryUsedForDownloadPerMonth')
    .then((res) => res.data);
};
