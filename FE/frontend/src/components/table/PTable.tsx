import { Table } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

const PTable: FC<{ columns: object[]; dataSource: object[] | undefined }> = ({
  columns,
  dataSource,
}) => {
  return <StyledTable dataSource={dataSource} columns={columns} />;
};

const StyledTable = styled(Table).attrs({
  'data-testid': 'table',
})`
  width: 100%;
  overflow: hidden;
  border-radius: 0.8rem;
  background-color: transparent;

  .ant-table-content {
    background-color: #f1f1f1 !important;
  }

  .ant-table-row {
    background-color: #fff !important;
  }

  .ant-table-cell {
    border-radius: 0.8rem;
  }

  .ant-table-thead > tr > th {
    height: 60px;
    font-size: 18px;
    padding: 8px;
    background-color: ${({ theme: { colors } }) => colors.primary};
    color: white;
    border-bottom-left-radius: 0.8rem;
    border-bottom-right-radius: 0.8rem;
  }
`;

export default PTable;
