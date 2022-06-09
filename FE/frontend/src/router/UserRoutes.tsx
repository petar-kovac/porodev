import { FC, ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const UserRoutes: FC<{ children?: ReactNode; isUser: boolean }> = ({
  children,
  isUser,
}) => {
  return isUser ? <Outlet /> : <Navigate to="/notallowed" />;
};

export default UserRoutes;
