import { Layout } from 'antd';
import { FC, useState } from 'react';
import styled from 'styled-components';

import { useAuthStateValue } from 'context/AuthContext';

const { Sider } = Layout;

const PFileSider: FC = () => {
  return (
    <StyledFileSider>
      <div>a</div>
    </StyledFileSider>
  );
};

const StyledFileSider = styled(Sider)`
  margin-top: 20px;
  background-color: #fff;
  display: flex;

  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
  }

  .ant-layout-sider-children {
    display: flex;
    flex-direction: column;
  }

  & li span {
    font-size: 1.9rem;
  }

  & li span svg {
    font-size: 1.9rem;
  }

  .ant-layout-sider-trigger {
    background-color: ${({ theme: { colors } }) => colors.primary};
  }
`;

export default PFileSider;
