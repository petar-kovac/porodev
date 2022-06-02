import React, { FC, useState } from 'react';
import { Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

const { Sider } = Layout;

const PSider: FC = () => {
  const [collapsed, setCollapsed] = useState<boolean | undefined>(
    localStorage.getItem('collapsedMenu') === 'true',
  );

  return (
    <StyledSider
      collapsible
      collapsed={collapsed}
      style={{ color: 'white' }}
      onCollapse={(value: boolean) => {
        localStorage.setItem('collapsedMenu', value.toString());
        setCollapsed(collapsed);
      }}
    >
      <Menu>
        <Menu.Item key={1}>
          <Link to="/">Home</Link>
        </Menu.Item>
        <Menu.Item key={2}>
          <Link to="/test">Test</Link>
        </Menu.Item>
      </Menu>
    </StyledSider>
  );
};

const StyledSider = styled(Sider)`
  background-color: #fff;
`;

export default PSider;
