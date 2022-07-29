import {
  FieldTimeOutlined,
  FileOutlined,
  FontColorsOutlined,
  HomeOutlined,
  TeamOutlined,
  UserOutlined,
} from '@ant-design/icons';
import { Avatar, Layout, Menu } from 'antd';
import { FC, useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import styled from 'styled-components';

import { Airplane } from '@styled-icons/ionicons-outline';
import { useAuthStateValue } from 'context/AuthContext';
import { usePageContext } from 'context/PageContext';
import { Role } from 'util/enums/roles';

const { Sider } = Layout;

const PSider: FC = () => {
  const { isCollapsed, setIsCollapsed } = usePageContext();

  const { pathname } = useLocation();

  const { isAdmin, loggedUser } = useAuthStateValue();

  return (
    <StyledSider
      collapsible
      collapsed={isCollapsed}
      trigger={
        <StyledTriggerContainer>
          <StyledTriggerIcon>
            <StyledAirplane isCollapsed={isCollapsed} />
          </StyledTriggerIcon>
        </StyledTriggerContainer>
      }
      onCollapse={(collapsed: boolean) => {
        localStorage.setItem('collapsedMenu', collapsed.toString());
        setIsCollapsed(collapsed);
      }}
    >
      <StyledSiderHeader>
        <Avatar
          size={60}
          src="https://joeschmoe.io/api/v1/random"
          style={{ border: '1.5px solid #ccc' }}
        />
      </StyledSiderHeader>

      <StyledSiderMenu
        mode="inline"
        style={{ height: '144px' }}
        selectedKeys={[pathname]}
      >
        {loggedUser === Role.ADMIN && (
          <>
            <Menu.Item key="/" icon={<HomeOutlined />}>
              <Link to="/">Home</Link>
            </Menu.Item>
            {/* <Menu.Item key="/admins" icon={<FontColorsOutlined />}>
              <Link to="/admins">Admins</Link>
            </Menu.Item> */}
            <Menu.Item key="/users" icon={<UserOutlined />}>
              <Link to="/users">Users</Link>
            </Menu.Item>
            <Menu.Item key="/files" icon={<FileOutlined />}>
              <Link to="/files">Files</Link>
            </Menu.Item>
            <Menu.Item key="/groups" icon={<TeamOutlined />}>
              <Link to="/groups">Groups</Link>
            </Menu.Item>
            {/* <Menu.Item key="/runtime" icon={<FieldTimeOutlined />}>
              <Link to="/runtime">Runtime</Link>
            </Menu.Item> */}
          </>
        )}
        {loggedUser === Role.USER && (
          <>
            <Menu.Item key="/user-home" icon={<HomeOutlined />}>
              <Link to="/user-home">Home</Link>
            </Menu.Item>
            <Menu.Item key="/user-files" icon={<FileOutlined />}>
              <Link to="/user-files">Files</Link>
            </Menu.Item>
            <Menu.Item key="/user-groups" icon={<TeamOutlined />}>
              <Link to="/user-groups">Groups</Link>
            </Menu.Item>
            <Menu.Item key="/user-runtime" icon={<FieldTimeOutlined />}>
              <Link to="/user-runtime">Runtime</Link>
            </Menu.Item>
          </>
        )}
      </StyledSiderMenu>

      <StyledSiderFooter>
        Copyright 2022 &copy; PoroDev. <br /> All Rights Reserved
      </StyledSiderFooter>
    </StyledSider>
  );
};

const StyledAirplane = styled(Airplane)<{ isCollapsed: boolean | undefined }>`
  height: ${({ isCollapsed }) => (isCollapsed ? '20px' : '25px')};
  transform: ${({ isCollapsed }) =>
    isCollapsed ? 'rotate(0)' : 'rotate(-180deg)'};
  transition: 0.3s;
`;

const StyledTriggerIcon = styled.div`
  width: 50px;
  height: 50px;
`;

const StyledTriggerContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;

const StyledSider = styled(Sider).attrs({
  'data-testid': 'sider',
})`
  background-color: #fff;
  display: flex;
  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
    border-top-right-radius: 0.8rem;
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

const StyledSiderHeader = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 1rem;
  margin: 2rem 0 3rem 0;
`;

const StyledSiderMenu = styled(Menu)``;

const StyledSiderFooter = styled.div`
  color: #999;
  font-size: 12px;
  text-align: center;
  margin-top: auto;
  margin-bottom: 1rem;
`;

export default PSider;
