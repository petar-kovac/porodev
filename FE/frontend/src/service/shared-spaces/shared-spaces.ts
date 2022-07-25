import api from '../base';
import { ICreateSpaceRequest } from './shared-spaaces.props';

export const createSharedSpace: (payload: string) => Promise<any> = (
  payload,
) => {
  return api
    .service()
    .post('/api/SharedSpace/Create', payload)
    .then((res) => res.data);
};

export const addUserToSharedSpace: (payload: any) => Promise<any> = (
  payload,
) => {
  return api
    .service()
    .post('/api/SharedSpace/AddUserToSharedSpace', payload)
    .then((res) => res.data);
};

export const getAllUsersFromSharedSpace: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/SharedSpace/GetAllUsersInSharedSpace')
    .then((res) => res.data);
};

export const addFile: (payload: ICreateSpaceRequest) => Promise<any> = (
  payload,
) => {
  return api
    .service()
    .post('/api/SharedSpace/AddFile', payload)
    .then((res) => res.data);
};

export const getAllFiles: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/SharedSpace/GetAllFiles')
    .then((res) => res.data);
};
export const getAllSharedSpaces: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/User/ReadAllSharedSpacesForUser')
    .then((res) => res.data);
};
export const readAllUsers: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/User/ReadAllUsers')
    .then((res) => res.data);
};
