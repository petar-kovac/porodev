import React, { FC, useState } from 'react';
import { Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
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
      style={{ color: 'white' }}
      onCollapse={(collapsed: boolean) => {
        localStorage.setItem('collapsedMenu', collapsed.toString());
        setIsCollapsed(collapsed);
      }}
    >
      <Menu>
        <Menu.Item key={1}>
          <Link to="/">Home</Link>
        </Menu.Item>
        <Menu.Item key={2}>
          <Link to="/test">Test</Link>
        </Menu.Item>
        {isAdmin && (
          <Menu.Item key={3}>
            <Link to="/adminpage">Adminpage</Link>
          </Menu.Item>
        )}

        <Menu.Item key={3}>
          <Link to="/files">Files</Link>
        </Menu.Item>
      </Menu>
    </StyledSider>
  );
};

const StyledSider = styled(Sider)`
  background-color: #fff;
`;

export default PSider;
