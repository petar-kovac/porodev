import { FC } from 'react';
import { Layout } from 'antd';

const { Content } = Layout;

const PContent: FC<any> = ({ children }) => {
  return <Content style={{ color: 'black' }}>{children} </Content>;
};

export default PContent;
