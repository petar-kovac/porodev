import api from '../base';

export const getAllUsers: () => Promise<any> = () => {
  return api
    .service()
    .get('/api/User/ReadAllUsers')
    .then((res) => res);
};
