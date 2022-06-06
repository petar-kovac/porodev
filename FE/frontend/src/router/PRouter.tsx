import { FC, useState } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import styled from 'styled-components';
import { Layout } from 'antd';
import { useAuthStateValue } from '../context/AuthContext';
import Home from '../pages/home/Home';
import Test from '../pages/home/Test';
import AdminPage from '../pages/home/AdminPage';
import PContent from '../layout/content/PContent';
import PHeader from '../layout/header/PHeader';
import PSider from '../layout/sider/PSider';
import Login from '../pages/Login';
import Profile from '../pages/profile/Profile';
import PProtectedRoute from './PProtectedRoute';
import Error from '../pages/error/ErrorPage';
import Files from '../pages/files/Files';

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
                <Route path="/test" element={<Test />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/files" element={<Files />} />
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
  return <h1>loading</h1>;
};

export default PRouter;
const StyledLayout = styled(Layout)`
  height: 100vh;
`;
