import { FC, useState } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import styled from 'styled-components';
import { Layout } from 'antd';
import { useAuthStateValue } from '../context/AuthContext';
import Home from '../pages/home/Home';
import AdminPage from '../pages/home/AdminPage';
import PContent from '../layout/content/PContent';
import PHeader from '../layout/header/PHeader';
import PSider from '../layout/sider/PSider';
import Login from '../pages/login/Login';
import Profile from '../pages/profile/Profile';
import PProtectedRoute from './PProtectedRoute';
import Error from '../pages/error/ErrorPage';
import Files from '../pages/files/Files';
import Users from '../pages/users/Users';
import Admins from '../pages/admins/Admins';
import Spinner from '../components/spinner/Spinner';

const PRouter: FC = () => {
  const { isAuthenticated, isLoading } = useAuthStateValue();
  if (!isLoading) {
    if (isAuthenticated) {
      return (
        <StyledLayout>
          <PHeader />
          <Layout>
            <PSider />
            <PContent>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/files" element={<Files />} />
                <Route path="/users" element={<Users />} />
                <Route path="/admins" element={<Admins />} />
                <Route
                  path="*"
                  element={<Error message="Router error 404" />}
                />
                <Route
                  path="/adminpage"
                  element={
                    <PProtectedRoute>
                      <AdminPage />
                    </PProtectedRoute>
                  }
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
