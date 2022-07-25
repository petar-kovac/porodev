import { render, screen } from '@testing-library/react';

import PButton from 'components/buttons/PButton';

describe('Login button', () => {
  it('should have type attribute', () => {
    const type = 'primary';
    render(<PButton type={type} />);

    expect(screen.getByTestId('undefined-button')).toBeEnabled();
  });
});
