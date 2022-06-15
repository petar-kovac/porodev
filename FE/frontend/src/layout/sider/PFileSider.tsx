import { Button, Layout } from 'antd';
import { Dispatch, FC, SetStateAction, useState } from 'react';
import styled from 'styled-components';

import { CloseOutlined } from '@ant-design/icons';

const { Sider } = Layout;

interface IPFileSiderProps {
  title?: string;
  content?: string;
  isSiderVisible?: boolean;
  setIsSiderVisible: Dispatch<SetStateAction<boolean>>;
}

const PFileSider: FC<IPFileSiderProps> = ({
  title,
  content,
  isSiderVisible,
  setIsSiderVisible,
}) => {
  const handleClose = () => {
    setIsSiderVisible(false);
  };

  return (
    <StyledFileSider collapsedWidth={0} collapsed={!isSiderVisible} width={320}>
      <StyledSiderWrapper>
        <StyledHeading>
          <StyledRow>
            <div>{title}</div>
            <CloseOutlined onClick={() => handleClose()} />
          </StyledRow>
        </StyledHeading>
        <div>{content}</div>
      </StyledSiderWrapper>
    </StyledFileSider>
  );
};

const StyledRow = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

const StyledSiderWrapper = styled.div`
  position: fixed;
  display: flex;
  flex-direction: column;
  width: 100%;
  gap: 10px;
  padding: 20px;
`;

const StyledHeading = styled.div`
  display: flex;
  width: 100%;
  font-size: 20px;
`;
const StyledFileSider = styled(Sider).attrs({
  'data-testid': 'file-sider',
})`
  background-color: #fff;
  overflow-x: hidden;
  display: flex;

  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
  }

  .ant-layout-sider-children {
    display: flex;
    flex-direction: column;
    width: 400px;
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
