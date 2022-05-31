import React, { FC } from 'react';
import { Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';

const { Sider } = Layout;

const PSider: FC = () => {
  console.log('Sider');

  return (
    <Sider collapsible style={{ color: 'white' }}>
      <Menu>
        <Menu.Item>
          <Link to="/">Home</Link>
        </Menu.Item>
        <Menu.Item>
          <Link to="/test">Test</Link>
        </Menu.Item>
      </Menu>
    </Sider>
  );
};

export default PSider;
