import React, { FC } from 'react';
import { Layout } from 'antd';

const { Header } = Layout;

const PHeader: FC = () => {
  console.log('Header');

  return <Header style={{ color: 'white' }}>Header</Header>;
};

export default PHeader;
