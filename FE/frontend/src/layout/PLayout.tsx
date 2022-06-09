import { Layout } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

import PContent from './content/PContent';
import PHeader from './header/PHeader';
import PSider from './sider/PSider';

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
