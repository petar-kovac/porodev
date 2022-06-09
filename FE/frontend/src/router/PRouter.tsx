import { Layout } from 'antd';
import { FC } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import styled from 'styled-components';
import Spinner from '../components/spinner/Spinner';
import { useAuthStateValue } from '../context/AuthContext';
import PContent from '../layout/content/PContent';
import PHeader from '../layout/header/PHeader';
import PSider from '../layout/sider/PSider';
import Admins from '../pages/admin/admins/Admins';
import Error from '../pages/error/ErrorPage';
import Files from '../pages/admin/files/Files';
import UserFiles from '../pages/user/files/Files';
import AdminPage from '../pages/admin/home/AdminPage';
import Home from '../pages/admin/home/Home';
import UserHome from '../pages/user/home/Home';
import Login from '../pages/login/Login';
import Profile from '../pages/user/profile/Profile';
import Users from '../pages/admin/users/Users';
import AdminRoutes from './AdminRoutes';
import UserRoutes from './UserRoutes';

const PRouter: FC = () => {
  const { isAuthenticated, isLoading, isAdmin } = useAuthStateValue();

  if (!isLoading) {
    if (isAuthenticated) {
      return (
        <StyledLayout>
          <PHeader />
          <Layout>
            <PSider />
            <PContent>
              <Routes>
                <Route element={<AdminRoutes isAdmin={isAdmin} />}>
                  <Route path="/" element={<Home />} />
                  <Route path="/files" element={<Files />} />
                  <Route path="/admins" element={<Admins />} />
                  <Route path="/users" element={<Users />} />
                  <Route path="/adminpage" element={<AdminPage />} />
                </Route>
                <Route element={<UserRoutes isUser={!isAdmin} />}>
                  <Route path="/user-home" element={<UserHome />} />
                  <Route path="/user-files" element={<UserFiles />} />
                  <Route path="/profile" element={<Profile />} />
                </Route>
                <Route
                  path="/notallowed"
                  element={<Error message="Cant go here" />}
                />
                <Route
                  path="*"
                  element={<Error message="Router error 404" />}
                />
              </Routes>
            </PContent>
          </Layout>
        </StyledLayout>
      );
    }
    return (
      <Routes>
        <Route path="*" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    );
  }
  return (
    <StyledPage>
      <SpinnerWrapper>
        <Spinner color="#000" size={42} speed={2} />
      </SpinnerWrapper>
    </StyledPage>
  );
};

export default PRouter;

const SpinnerWrapper = styled.div`
  height: 200px;
  width: 200px;
  display: flex;
  justify-content: center;
  align-items: center;
`;

const StyledPage = styled.div`
  height: 100vh;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f0f2f5;
`;
const StyledLayout = styled(Layout)`
  height: 100vh;
`;
