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

  .ant-table-thead > tr > th {
    height: 60px;
    font-size: 18px;
    padding: 8px;
    background-color: ${({ theme: { colors } }) => colors.primary};
    color: white;
  }
`;

export default PTable;
