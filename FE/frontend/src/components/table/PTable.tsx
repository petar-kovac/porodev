import { Table } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';
// import Spinner from '../spinner/Spinner';
// const antIcon = <LoadingOutlined style={{ fontSize: 24 }} spin />;
const PTable: FC<{ columns: object[]; dataSource: object[] | undefined }> = ({
  columns,
  dataSource,
}) => {
  return (
    <StyledTable
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

const StyledTable = styled(Table)`
  width: 100%;
  overflow: hidden;

  .ant-table-thead > tr > th {
    height: 60px;
    font-size: 18px;
    padding: 8px;
    background-color: ${({ theme: { colors } }) => colors.primary};
    color: white;
  }
`;

export default PTable;
