import { FC } from 'react';
import { Area } from '@ant-design/plots';
import styled from 'styled-components';

interface IDashboardProps {
  data: object[];
}

const StackedArea: FC<IDashboardProps> = ({ data }) => {
  const config = {
    data,
    xField: 'date',
    yField: 'value',
    seriesField: 'department',
    line: {
      style: {
        cursor: 'pointer',
        shadowColor: '#888',
        shadowBlur: 10,
        shadowOffsetX: 5,
        shadowOffsetY: 5,
        strokeOpacity: 0.7,
      },
    },
  };

  return (
    <StyledStackedArea>
      <Area {...config} />
    </StyledStackedArea>
  );
};

const StyledStackedArea = styled.div`
  border: 1px solid #ddd;
  border-radius: 2rem;
  padding: 3rem;
  background-color: #fff;
`;

export default StackedArea;
