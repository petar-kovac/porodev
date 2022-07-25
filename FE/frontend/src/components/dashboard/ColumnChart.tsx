import { FC } from 'react';
import { Column } from '@ant-design/charts';
import styled from 'styled-components';

interface IColumnChartProps {
  columnData: object[];
}

const ColumnChart: FC<IColumnChartProps> = ({ columnData }) => {
  const data = columnData;
  const config = {
    data,
    xField: 'day',
    yField: 'value',
    seriesField: '',

    xAxis: {
      label: {
        autoHide: true,
        autoRotate: false,
      },
    },
  };
  return (
    <StyledColumnChart>
      <Column {...config} />
    </StyledColumnChart>
  );
};

const StyledColumnChart = styled.div`
  border: 1px solid #ddd;
  border-radius: 2rem;
  padding: 3rem;
  background-color: #fff;
`;

export default ColumnChart;
