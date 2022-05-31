import { FC, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import styled from 'styled-components';
import { Layout } from 'antd';
import { useAuthStateValue } from '../context/AuthContext';
import Home from '../pages/home/Home';
import Test from '../pages/home/Test';
import PContent from '../layout/content/PContent';
import PHeader from '../layout/header/PHeader';
import PSider from '../layout/sider/PSider';
import Login from '../pages/login/Login';

const PRouter: FC = () => {
  const { isAuthenticated } = useAuthStateValue();
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
            </Routes>
          </PContent>
        </Layout>
      </StyledLayout>
    );
  }
  return <Login />;
};

export default PRouter;
const StyledLayout = styled(Layout)`
  height: 100vh;
`;
