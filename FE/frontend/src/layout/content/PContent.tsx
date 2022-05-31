import { FC } from 'react';
import { Layout } from 'antd';
// import PRouter from '../../router/PRouter';

const { Content } = Layout;
// interface IProps {
//   message: string;
//   children: any;
// }

const PContent: FC<any> = ({ children }) => {
  return <Content style={{ color: 'black' }}>{children} </Content>;
};

export default PContent;
