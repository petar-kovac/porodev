import { useState, useEffect, FC } from 'react';
import ReactDOM from 'react-dom';
import { Column } from '@ant-design/charts';

interface IColumnChartProps {
  columnData: object[];
}

const ColumnChart: FC<IColumnChartProps> = ({ columnData }) => {
  const paletteSemanticRed = '#F4664A';
  const brandColor = '#5B8FF9';
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
  return <Column {...config} />;
};

export default ColumnChart;
