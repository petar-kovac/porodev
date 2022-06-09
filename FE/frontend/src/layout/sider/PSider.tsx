import { FC, useEffect, useState } from 'react';
import { Layout, Menu, Avatar } from 'antd';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import {
  AlertOutlined,
  AppstoreOutlined,
  FileOutlined,
  FontColorsOutlined,
  HomeOutlined,
  MailOutlined,
  SettingOutlined,
  UserOutlined,
} from '@ant-design/icons';
import { useAuthStateValue } from '../../context/AuthContext';

const { Sider } = Layout;

const PSider: FC = () => {
  const [isCollapsed, setIsCollapsed] = useState<boolean | undefined>(
    localStorage.getItem('collapsedMenu') === 'true',
  );
  const { isAdmin } = useAuthStateValue();

  return (
    <StyledSider
      collapsible
      collapsed={isCollapsed}
      onCollapse={(collapsed: boolean) => {
        localStorage.setItem('collapsedMenu', collapsed.toString());
        setIsCollapsed(collapsed);
      }}
    >
      <StyledSiderHeader>
        <Avatar size={60} src="https://joeschmoe.io/api/v1/random" />
        <h1>Dashboard</h1>
      </StyledSiderHeader>

      <StyledSiderMenu mode="inline" style={{ height: '144px' }}>
        {isAdmin && (
          <>
            <Menu.Item key={1} icon={<HomeOutlined />}>
              <Link to="/">Home</Link>
            </Menu.Item>
            <Menu.Item key={2} icon={<FontColorsOutlined />}>
              <Link to="/admins">Admins</Link>
            </Menu.Item>
            <Menu.Item key={3} icon={<UserOutlined />}>
              <Link to="/users">Users</Link>
            </Menu.Item>
            <Menu.Item key={4} icon={<FileOutlined />}>
              <Link to="/files">Files</Link>
            </Menu.Item>
          </>
        )}
        {!isAdmin && ( // later change it to isUser when backend is implemented
          <>
            <Menu.Item key={5} icon={<HomeOutlined />}>
              <Link to="/user-home">Home</Link>
            </Menu.Item>
            <Menu.Item key={6} icon={<FileOutlined />}>
              <Link to="/user-files">Files</Link>
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

const StyledSider = styled(Sider)`
  background-color: #fff;
  display: flex;
  flex-direction: column;

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
