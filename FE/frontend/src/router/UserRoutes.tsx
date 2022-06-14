import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const UserRoutes: FC<{ isUser: boolean }> = ({ isUser }) => {
  return isUser ? <Outlet /> : <Navigate to="/notallowed" />;
};

export default UserRoutes;
