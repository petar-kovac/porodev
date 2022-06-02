import { FC, ReactNode } from 'react';
import { Layout } from 'antd';

const { Content } = Layout;

const PContent: FC<{ children: ReactNode }> = ({ children }) => {
  return <Content style={{ color: 'black' }}>{children} </Content>;
};

export default PContent;
