import React, { FC } from 'react';
import styled from 'styled-components';
import { Layout } from 'antd';
import PHeader from './header/PHeader';
import PSider from './sider/PSider';
import PContent from './content/PContent';

const PLayout: FC = () => (
  <StyledLayout>
    <PHeader />
    <Layout>
      <PSider />
      <PContent> Test</PContent>
    </Layout>
  </StyledLayout>
);

export default PLayout;

const StyledLayout = styled(Layout)`
  height: 100vh;
`;