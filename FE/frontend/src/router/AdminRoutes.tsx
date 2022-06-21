import { FC, ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const AdminRoutes: FC<{
  isAdmin: boolean;
  location: any;
}> = ({ isAdmin, location }) => {
  console.log(location.pathname);

  return isAdmin ? (
    <Outlet />
  ) : location.pathname === '/' ? (
    <Navigate to="/user-home" />
  ) : (
    <Navigate to="/notallowed" />
  );
};

export default AdminRoutes;
