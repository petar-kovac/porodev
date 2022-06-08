import { LoadingOutlined } from '@ant-design/icons';
import { Table } from 'antd';
import { FC } from 'react';
// import Spinner from '../spinner/Spinner';
// const antIcon = <LoadingOutlined style={{ fontSize: 24 }} spin />;
const PTable: FC<{ columns: object[]; dataSource: object[] | undefined }> = ({
  columns,
  dataSource,
}) => {
  return (
    <Table
      // loading={{
      //   indicator: (
      //     <div>
      //       <Spinner color="#000" size={24} speed={1} />
      //     </div>
      //   ),
      // }}
      dataSource={dataSource}
      columns={columns}
    />
  );
};

export default PTable;
